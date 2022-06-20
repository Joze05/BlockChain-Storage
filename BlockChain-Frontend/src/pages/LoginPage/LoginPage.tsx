import React from "react";
import "./LoginPage.css";
import Logo from "../../components/Logo/Logo";
import LoginForm from "../../components/LoginForm/LoginForm";

function LoginPage() {
  return (
    <div className="login-page-container">
      <header className="logo-container">
        <Logo></Logo>
      </header>
      <div className="login-form-container">
          <LoginForm></LoginForm>
      </div>
    </div>
  );
}

export default LoginPage;
