import React, { useState } from "react";
import "./LoginForm.css";
import { useLocation } from "wouter";
import md5 from "md5";

const API_USER_URL = "http://localhost:5086/api/User/";


function LoginForm() {

  const [form, setForm] = useState({});


  async function checkUser(data: any) {

    data.password = md5(data.password);
    fetch(API_USER_URL + "validate", {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((result) => result.json())
      .then((data) => {
        if (data != null) {
          localStorage.setItem('userName', data.userName);

          setLocation(`/Dashboard`);

          //setLocation(`/user/${data.userName}`);
        } else {
          alert("Usuario o contraseña incorrecta");
          //console.log("Usuario o contraseña incorrecta");
        }
      });
  }

  const [location, setLocation] = useLocation();

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    setForm({
      ...form,
      [name]: value,
    });
  };

  return (
    <>
      <div className="login-form">
        <label>
          <h5>Username:</h5>
          <input
            type="text"
            name="userName"
            id="userName"
            className="input-text"
            placeholder="Elon"
            onChange={handleChange}
          />
        </label>

        <label>
          <h5>Password:</h5>
          <input
            type="password"
            name="password"
            id="password"
            className="input-text"
            placeholder="1234"
            onChange={handleChange}
          />
        </label>

        <button id="submit-button" onClick={() => checkUser(form)}>
          Iniciar Sesión
        </button>
      </div>
    </>
  );
}

export default LoginForm;