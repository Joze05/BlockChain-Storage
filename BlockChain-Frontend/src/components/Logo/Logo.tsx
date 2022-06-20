import React from "react";
import logo from '../../assets/blockchain-logo.png'
import './Logo.css'

function Logo(){


    return(

<div className="logo-container">

<img src={logo} alt="" />
<div className="app-name-container">
<h3>BlockChain Docs</h3>
<h3 id="dot">.</h3>
</div>

</div>

    )

}

export default Logo;