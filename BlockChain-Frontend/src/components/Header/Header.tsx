import React from "react";
import "./Header.css"

import { useLocation } from "wouter";
import Logo from "../../components/Logo/Logo";
import UserTag from "../../components/UserTag/UserTag";
import ConfigForm from "../../components/ConfigForm/ConfigForm";
import { Button } from "@mui/material";



export default function Header(params: any) {
    const [location, setLocation] = useLocation();

    function cerrarSesion() {
        localStorage.setItem("userName", "");
        setLocation(`/login`);
    }

    return (

        <header>

            <Logo></Logo>
            <ConfigForm></ConfigForm>
            <UserTag userName={params.userName}></UserTag>
            <Button onClick={() => cerrarSesion()}>Cerrar Sesión</Button>

        </header>

    )

}