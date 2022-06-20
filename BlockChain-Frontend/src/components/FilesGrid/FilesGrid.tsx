import React, { useEffect, useState } from "react";
import "../FilesGrid/FilesGrid.css";
import { deleteFile, getFiles } from "../../services/FileController";
import fileDownload from "js-file-download";
import { circularProgressClasses } from "@mui/material";
import { CLIENT_RENEG_WINDOW } from "tls";

export default function FilesGrid() {
  const [files, setFiles] = useState([]);

  useEffect(() => {
    getFiles().then((files) => setFiles(files));
  });


  function readFile(file) {
    return new Promise((resolve, reject) => {
      // Create file reader
      let reader = new FileReader()

      // Register event listeners
      reader.addEventListener("loadend", e => resolve(e.target.result))
      reader.addEventListener("error", reject)

      // Read file
      reader.readAsArrayBuffer(file)
    })
  }

  async function downloadFile(data: any, name: string, fileType: string) {

    console.log(data)
    // let arrayToString: string = '';
    // for (let i = 0; i < data.length; i++) {

    //   arrayToString += data[i];

    // }

    var base64enconded = btoa(data);

    const byteString = window.atob(base64enconded.split(",")[1]);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8array[i] = byteString.charCodeAt(1);
    }

    const blob = new Blob([int8array], { type: name.split(".")[1] })
    fileDownload(blob, name)

  }
  return (
    <div className="grid-container">
      <table className="files-table">
        <tbody>
          <tr>
            <th>Propietario</th>
            <th>Nombre Archivo</th>
            <th>Extensión</th>
            <th>Fecha</th>
            <th>Size</th>
            <th>Acciones</th>
          </tr>

          {files.map((file) => (
            <tr key={file._id}>
              <td>{file.owner}</td>
              <td>{file.name}</td>
              <td>{file.extension}</td>
              <td>{file.date}</td>
              <td>{file.size}</td>
              <td>
                <button
                  onClick={() => {
                    deleteFile(file._id);
                  }}
                >
                  Eliminar
                </button>
                <button
                  onClick={() => {
                    downloadFile(file.fileContent, file.name + "." + file.extension, file.extension);
                  }}
                >
                  Descargar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
