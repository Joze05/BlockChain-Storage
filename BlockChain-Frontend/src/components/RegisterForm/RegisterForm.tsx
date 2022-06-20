import React from "react";
import "./RegisterForm.css";
import { useForm } from "react-hook-form";
import { insertUser } from "../../services/UserController";
import md5 from "md5";


function RegisterForm() {
  const { register, handleSubmit } = useForm();
  const onSubmit = (data: any) => {
    data.password = md5(data.password);
    insertUser((data))
  };

  return (
    <div className="form-container">
      <form className="register-form" onSubmit={handleSubmit(onSubmit)}>
        <label>
          <h5>Username:</h5>
          <input className="input-text" placeholder="ElonM-Usk" {...register("userName")} />
        </label>

        <label>
          <h5>Password:</h5>
          <input className="input-text" type="password" placeholder="1234" {...register("password")} />
        </label>

        <label>
          <h5>Name:</h5>
          <input className="input-text" placeholder="Elon" {...register("name")} />
        </label>

        <label>
          <h5>Lastname:</h5>
          <input className="input-text" placeholder="Musk" {...register("lastName")} />
        </label>

        <label id="email">
          <h5>Email:</h5>
          <input className="input-text" placeholder="elon@gmail.com" {...register("email")} />
        </label>

        <label>
          <h5>Birthday:</h5>
          <input type="date" className="input-text" {...register("birthday")} />
        </label>

        <input id="submit-button" type="submit" value="Crear cuenta" />
      </form>
    </div>
  );
}

export default RegisterForm;