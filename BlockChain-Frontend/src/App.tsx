import React from "react";
import { Route } from "wouter";
import LoginPage from "./pages/LoginPage/LoginPage";
import RegisterPage from "./pages/RegisterPage/RegisterPage";
import DashboardPage from "./pages/DashboardPage/DashboardPage";
export default function App() {
  return (
    <div className="App">

      <Route path="/" component={RegisterPage}></Route>
      <Route path="/login" component={LoginPage}></Route>

      <Route path="/Dashboard" component={DashboardPage}></Route>



    </div >
  );
}
