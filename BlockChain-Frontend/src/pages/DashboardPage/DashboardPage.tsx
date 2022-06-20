import React from "react";
import "./DashboardPage.css";
import Header from "../../components/Header/Header";
import UploadFileChooser from "../../components/FileForm/UploadFileChooser";
import FilesGrid from "../../components/FilesGrid/FilesGrid";
import { Redirect } from "wouter";
import LoginForm from "../../components/LoginForm/LoginForm";

export default function DashboardPage() {

  if (localStorage.getItem("userName").toString().length > 0) {
    return (
      <div className="main-dashboard-container">
        <Header userName={localStorage.getItem('userName').toString()}></Header>
        <div className="dahsboard-body">
          <UploadFileChooser userName={localStorage.getItem('userName').toString()} />
          <FilesGrid />
        </div>
        <footer>@All rights reserved 2022</footer>
      </div>
    );
  } else {
    return (
      <div className="dahsboard-body">
        <a href="http://localhost:3000/login"> Ingrese sesión por favor</a>
      </div>

    );
  }


}
