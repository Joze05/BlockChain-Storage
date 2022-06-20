import "./RegisterPage.css";
import Logo from "../../components/Logo/Logo";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import { Link } from "wouter";

function RegisterPage() {

  return (
    <div className="RegisterPage">

      <div className="logo-container">
        <Logo></Logo>
      </div>

      <div className="headers-container">
        <h3>START FOR FREE</h3>
        <h1 > <p className="main-header-container"> Create new account </p></h1>
        <div className="login-router-container">
          <h4>Already A Member?</h4>
          <Link to="/login">Log In</Link>
        </div>
      </div>

      <div className="form-container">
        <RegisterForm></RegisterForm>
      </div>
    </div >
  );
}

export default RegisterPage;
