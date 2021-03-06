const API_FILES_URL = "http://localhost:5086/api/Files/";

export function insertFile(data: any) {
  return fetch(API_FILES_URL + localStorage.getItem("userName"), {
    method: "POST",
    body: data,
  })
    .then((response) => {
      return response.status;
    })
    .catch((error) => {
      console.log(error);
    });
}

export async function getFiles() {
  return fetch(API_FILES_URL + "userName/" + localStorage.getItem("userName"))
    .then((response) => response.json())
    .then((data) => {
      return data;
    })
    .catch((error) => {
      console.log(error);
    });
}

export async function getMinado(owner:string) {
   fetch(API_FILES_URL + "minar", {
    method: "POST",
    body: JSON.stringify(owner),
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then((response) => response.json())
    .then((data) => {
      return alert(data);
    })
    .catch((error) => {
      console.error("Mal: "+error);
    });
}

export async function deleteFiles(data: any) {
  fetch(API_FILES_URL, {
    method: "DELETE",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      alert(data);
    })
    .catch((error) => {
      console.log(error);
    });
}
