import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  insertConfig,
  getConfig,
  deleteConfig,
} from "../../services/UserController";
import "./ConfigForm.css";

export default function ConfigForm() {
  const { register, handleSubmit } = useForm();

  const onSubmit = (data: any) => {
    data.owner = localStorage.getItem("userName");
    insertConfig(data);
    deleteConfig(localStorage.getItem("userName"));
  };

  return (
    <div className="form-config-container">
      <form className="config-form" onSubmit={handleSubmit(onSubmit)}>
        <label className="label-config-form">
          <h5>Nombre configuracion</h5>
          <select className="drop-menu" {...register("configName")}>
            <option value="bloques">Files per Block</option>
          </select>
        </label>
        <label className="label-config-form">
          <h5>Valor configuracion</h5>
          <input className="input-text" {...register("configValue")} />
        </label>

        <input id="submit-button" type="submit" value="Save" />
      </form>
    </div>
  );
}
