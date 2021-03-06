const API_USER_URL = "http://localhost:5086/api/User/";
const API_CONFIG_URL = "http://localhost:5086/api/Config/";

/*
export async function getSingleUser(data:any) {
  fetch(API_USER_URL+data.userName+"/"+data.password)
    .then((result) => result.json()).then((data) => {return console.log("asda")})
}
*/
export function getAllusers() {
  fetch(API_USER_URL)
    .then((result) => result.json())
    .then((data) => console.log(data));
}

export function insertUser(data:any) {
  fetch(API_USER_URL, {
    method: "POST",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((result) => result.json())
    .then((data) => alert(data));
}

export function insertConfig(data:any){
fetch(API_CONFIG_URL,{
method: "POST",
body: JSON.stringify(data),
headers: {
  "Content-Type" : "application/json"
},
})
.then((result) => result.json())
.then((data) => alert(data));
}

export async function getConfig(owner:any){

  return fetch(API_CONFIG_URL + "getOwnerConfig", {
    method: "POST",
    body: JSON.stringify(owner),
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then((response) => response.json())
    .then((data) => {
      return data;
    })
    .catch((error) => {
      console.log(error);
    });

}

export function deleteConfig(data: any) {
  fetch(API_CONFIG_URL, {
    method: "DELETE",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => response.status)
    .catch((error) => {
      console.log(error);
    });
}
