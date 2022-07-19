import React, { useEffect, useState } from "react";
import "../FilesGrid/FilesGrid.css";
import { getBlocks } from "../../services/BlockController";
import fileDownload from "js-file-download";
import DeleteFilesButton from "../DeleteFilesButton/DeleteFilesButton";
import DownloadFilesButton from "../DownloadFilesButton/DownloadFilesButton";
import JSZip from "jszip"
import { saveAs } from "file-saver";
//import { Alerts } from "../../assets/Alerts";


import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Stack } from "react-bootstrap";




export default function BlocksGrid() {
  const [blocks, setBlocks] = useState([]);
  const [selectedBlocks] = useState([]);
  //const alertas = new Alerts();

  useEffect(() => {
    getBlocks().then((blocks) => setBlocks(blocks));
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

  function handleSelectedBlocks(event: any, block: any) {

    if (event.target.checked) {
      selectedBlocks.push(block);
    } else {
      const index = selectedBlocks
        .map(function (e) {
          return e._id;
        })
        .indexOf(block._id);
      selectedBlocks.splice(index, 1);
    }
  }

  /*async function mineFiles() {
    let data = await startMining();
    console.log(data);
    // alertas.message("", "Minado Exitoso", "success", 2000);
  }*/


  function convertStringToBlob(content: string, extension: string) {
    const byteString = window.atob(content);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    return new Blob([int8Array], { type: extension });
  }

  async function downloadMasive(fileList: any) {

    var zip = new JSZip();
    for (let fileItem of fileList) {
      let file = new File([convertStringToBlob(fileItem.fileContent, fileItem.extension)], fileItem.name, null);
      zip.file(fileItem.name + '.' + fileItem.extension, file);
    }
    zip.generateAsync({ type: "blob" }).then(content => {
      saveAs(content, "Documents.zip");
    });
    //fileListMasive.current = null;
  }

  return (

    <div className="grid-container">
      <Stack direction="horizontal" gap={2}>



      </Stack>


      <table className="files-table">
        <tbody>
          <tr>
            <th>Select</th>
            <th>Propietario</th>
            <th>Prueba</th>
            <th>Fecha Minado</th>
            <th>HashPrevio</th>
            <th>HashActual</th>
            <th>Acciones</th>
          </tr>

          {blocks.map((block) => (
            <tr key={block._id}>
              <td>
                <input
                  type="checkbox"
                  onClick={(e) => {
                    handleSelectedBlocks(e, block);
                  }}
                />
              </td>
              <td>{block.owner}</td>
              <td>{block.prueba}</td>
              <td>{block.fechaMinado}</td>
              <td>{block.hashPrevio}</td>
              <td>{block.hash}</td>
              <td>
                <Stack direction="horizontal" gap={2}>
                  <Button
                    className="btn mx-1"
                    as="a" variant="success"
                    id="download-button"
                    onClick={() => {

                      downloadMasive(block.fileList);

                    }}
                  >
                    <img src="https://img.icons8.com/ios-filled/30/000000/installing-updates--v2.png" />
                  </Button>
                </Stack>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="masive-action-button-container">

      </div>


    </div >
  );
}
