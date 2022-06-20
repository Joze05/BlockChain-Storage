import React from "react";
import { useForm } from "react-hook-form";
import { insertConfig } from "../../services/UserController";
import "./ConfigForm.css";

export default function ConfigForm() {
  const { register, handleSubmit } = useForm();
  const onSubmit = (data: any) => {
    //console.log(data);
    insertConfig(data);
  };

  return (
    <div className="form-config-container">
      <form className="config-form" onSubmit={handleSubmit(onSubmit)}>
        <label>
          <h5>Nombre configuracion</h5>
          <input className="input-text" {...register("configName")} />
        </label>
        <label>
          <h5>Valor configuracion</h5>
          <input className="input-text" {...register("configValue")} />
        </label>

        <input id="submit-button" type="submit" value="Save" />
      </form>
    </div>
  );
}
