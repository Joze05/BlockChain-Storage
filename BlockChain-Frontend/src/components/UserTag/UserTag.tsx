import React from "react";
import "./UserTag.css";

export default function UserTag(params: any) {
  return (
    <picture>
      <h3>{params.userName}</h3>
      <img
        src="https://randomuser.me/api/portraits/men/51.jpg"
        alt=""
        className="profile-picture"
      />
    </picture>
  );
}
