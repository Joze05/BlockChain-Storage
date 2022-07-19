import { dividerClasses } from "@mui/material";
import React from "react";
import "../DeleteFilesButton/DeleteFilesButton.css";
import { deleteFiles } from "../../services/FileController";
import Button from 'react-bootstrap/Button';

function DeleteFilesButton(props: any) {
  function emptySelectedFiles() {
    for (let i = props.elements.length; i > 0; i--) {
      props.elements.pop();
    }
  }

  return (
    <>
      <Button variant="danger"
        id="boton-eliminar-seleccion"
        onClick={() => {
          deleteFiles(props.elements);
          emptySelectedFiles();
        }}
      >
        Eliminar seleccion
        <img src="https://img.icons8.com/ios/25/undefined/delete-forever--v1.png" />
      </Button>
    </>
  );
  //{props.elements}
}

export default DeleteFilesButton;
