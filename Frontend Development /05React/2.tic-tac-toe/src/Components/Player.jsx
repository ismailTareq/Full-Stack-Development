import { useState } from "react";

export default function Player({initalname,symbol,isActive}) {
    const [playername,setplayername] = useState(initalname);
    const [isEditing,setisEditing] = useState(false);

    function handleditclick(){
        setisEditing((editing) => !isEditing)
    }

    function handleplayername(event){
        setplayername(event.target.value)
    }

    let editableplayername = <span className="player-name">{playername}</span>;

    if(isEditing)
    {
        editableplayername = <input type="text" required Value={playername} onChange={handleplayername}/>
    }

    return (
        <li className={isActive? 'active':undefined}>
            <span className="player">
              {editableplayername}
              <span className="player-symbol">{symbol}</span>
            </span>
            <button onClick={handleditclick}>{isEditing ? 'edit' : 'save'}</button>
        </li>
    );
}