const API_BLOCK_URL = "http://localhost:5086/api/Block/";

export async function getBlocks() {
  return fetch(API_BLOCK_URL+localStorage.getItem("userName"))
    .then((response) => response.json())
    .then((data) => {
      return data;
    })
    .catch((error) => {
      console.log(error);
    });
}

export async function getMinado(owner:string) {
   fetch(API_BLOCK_URL + "minar", {
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
  fetch(API_BLOCK_URL, {
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
