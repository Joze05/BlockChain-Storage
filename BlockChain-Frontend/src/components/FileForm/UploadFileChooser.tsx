import React, { useState } from "react";
import "./UploadFileChooser.css";
import { insertFile } from "../../services/FileController";

function UploadFileChooser(userName: any) {
  const [files, setFiles] = useState([]);
  //const [owner, setOwner] = useState(userName);

  const uploadFiles = (e) => {
    setFiles(e);
  };

  const insertFiles = async () => {
    const f = new FormData();

    for (let index = 0; index < files.length; index++) {
      //files[index].owner = "Josse";
      f.append("files", files[index]);
      //f.append("owner", userName)
    }
    //console.log(files);
    await insertFile(f, userName);
  };

  return (
    <div className="upload-input-container">
      <input
        type="file"
        name="files"
        multiple
        onChange={(e) => uploadFiles(e.target.files)}
      />
      <button onClick={() => insertFiles()}>Upload files</button>
    </div>
  );
}

export default UploadFileChooser;
