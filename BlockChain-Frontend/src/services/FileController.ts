import { json } from "stream/consumers";

const API_FILES_URL = "http://localhost:5086/api/Files/";

export function insertFile(data: any) {
  fetch(API_FILES_URL+localStorage.getItem("userName"), {
    method: "POST",
    body: data,
  })
    .then((response) => console.log(response))
    .catch((error) => {
      console.log(error);
    });
}

export async function getFiles() {
  return fetch(API_FILES_URL)
    .then((response) => response.json())
    .then((data) => {
      return data;
    })
    .catch((error) => {
      console.log(error);
    });
}

export async function deleteFile(id: string) {

  fetch(API_FILES_URL, {
    method: "DELETE",
    body: JSON.stringify(id),
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      return data;
    })
    .catch((error) => {
      console.log(error);
    });
}
