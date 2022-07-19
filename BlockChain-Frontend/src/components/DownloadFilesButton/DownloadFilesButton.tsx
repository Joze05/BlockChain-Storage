import React from "react";
import "./DownloadFilesButton.css";
//import JSZip from "jszip";
import JSZip from "jszip"
import { saveAs } from "file-saver";
import Button from 'react-bootstrap/Button';

function DownloadFilesButton(props: any) {

  function convertStringToBlob(content: string, extension: string) {
    const byteString = window.atob(content);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    return new Blob([int8Array], { type: extension });
  }

  async function downloadMasive() {

    var zip = new JSZip();
    for (let fileItem of props.elements) {
      let file = new File([convertStringToBlob(fileItem.fileContent, fileItem.extension)], fileItem.name, null);
      zip.file(fileItem.name + '.' + fileItem.extension, file);
    }
    zip.generateAsync({ type: "blob" }).then(content => {
      saveAs(content, "Documents.zip");
    });
    //fileListMasive.current = null;
  }

  return (
    <>
      <Button
        id="boton-descargar-seleccion"
        onClick={() => downloadMasive()}
      >
        Descargar seleccion
        <img src="https://img.icons8.com/windows/25/undefined/download--v1.png" />
      </Button>
    </>
  );
}

export default DownloadFilesButton;
