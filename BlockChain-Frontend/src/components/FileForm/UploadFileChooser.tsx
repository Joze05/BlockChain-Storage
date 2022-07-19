import React, { useState } from "react";
import "./UploadFileChooser.css";
import { insertFile } from "../../services/FileController";
import ClipLoader from "react-spinners/ClipLoader";
import Button from 'react-bootstrap/Button';

function UploadFileChooser(userName: any) {
  const [files, setFiles] = useState([]);
  const [loading, setLoading] = useState(false);

  const uploadFiles = (e) => {
    setFiles(e);
  };

  const insertFiles = async () => {
    const f = new FormData();

    setLoading(true);

    for (let index = 0; index < files.length; index++) {
      f.append("files", files[index]);
    }

    //var status = await insertFile(f);

    if (await insertFile(f) == 200) {
      setLoading(false);
    }
  };

  return (
    <div className="upload-input-container">
      <input
        type="file"
        name="files"
        multiple
        onChange={(e) => uploadFiles(e.target.files)}
      />
      <Button variant="info"
        onClick={() => insertFiles()}>Upload files</Button>
      {loading ? <ClipLoader></ClipLoader> : <></>}
    </div>
  );
}

export default UploadFileChooser;
