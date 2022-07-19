import React, { useEffect, useState } from "react";
import "../FilesGrid/FilesGrid.css";
import {
  deleteFiles,
  getFiles,
  getMinado,
} from "../../services/FileController";
import fileDownload from "js-file-download";
import DeleteFilesButton from "../DeleteFilesButton/DeleteFilesButton";
import DownloadFilesButton from "../DownloadFilesButton/DownloadFilesButton";
import { Stack } from "react-bootstrap";
import Button from 'react-bootstrap/Button';



export default function FilesGrid() {
  const [files, setFiles] = useState([]);
  const [selectedFiles] = useState([]);

  useEffect(() => {
    getFiles().then((files) => setFiles(files));
  });

  function readFile(file) {
    return new Promise((resolve, reject) => {
      // Create file reader
      let reader = new FileReader();

      // Register event listeners
      reader.addEventListener("loadend", (e) => resolve(e.target.result));
      reader.addEventListener("error", reject);

      // Read file
      reader.readAsArrayBuffer(file);
    });
  }

  function downloadFile(stringBase64: string, name: string, extension: string) {
    const byteString = window.atob(stringBase64);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }

    const blob = new Blob([int8Array], { type: extension });
    fileDownload(blob, name + "." + extension);
  }

  function handleSelectedFiles(event: any, file: any) {
    if (event.target.checked) {
      selectedFiles.push(file);
    } else {
      const index = selectedFiles
        .map(function (e) {
          return e._id;
        })
        .indexOf(file._id);
      selectedFiles.splice(index, 1);
    }
  }

  return (
    <div className="grid-container">
      <Stack direction="horizontal" gap={2}>

        <div className="masive-action-button-container">
          {selectedFiles[1] != null && (
            <DeleteFilesButton elements={selectedFiles} />
          )}

          {selectedFiles[1] != null && (
            <DownloadFilesButton elements={selectedFiles} />
          )}
        </div>

      </Stack>
      <Button variant="warning"
        id="btnMining"
        onClick={() => {
          var owner = localStorage.getItem("userName");
          getMinado(owner);
        }}
      >
        <img src="https://img.icons8.com/cotton/40/000000/golden-fever.png" />
      </Button>

      <table className="files-table">
        <tbody>
          <tr>
            <th>Select</th>
            <th>Propietario</th>
            <th>Nombre Archivo</th>
            <th>Extensión</th>
            <th>Fecha</th>
            <th>Size</th>
            <th>Acciones</th>
          </tr>

          {files.map((file) => (
            <tr key={file._id}>
              <td>
                <input
                  type="checkbox"
                  onClick={(e) => {
                    handleSelectedFiles(e, file);
                  }}
                />
              </td>
              <td>{file.owner}</td>
              <td>{file.name}</td>
              <td>{file.extension}</td>
              <td>{file.fileDate}</td>
              <td>{file.size}</td>
              <td>

                <Button
                  variant="danger"
                  id="delete-button"
                  onClick={() => {
                    deleteFiles([{ _id: file._id }]);
                  }}
                >
                  <img src="https://img.icons8.com/ios/25/undefined/delete-forever--v1.png" />
                </Button>
                <Button
                  variant="success"
                  id="download-button"
                  onClick={() => {
                    downloadFile(file.fileContent, file.name, file.extension);
                  }}
                >
                  <img src="https://img.icons8.com/windows/25/undefined/download--v1.png" />
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>




    </div>
  );
}
