import { useState, useRef } from "react";

export default function Player() {
  const [enteredPlayername, setenteredPlayername] = useState(null)
  const playername = useRef()

  function handlename(){
    setenteredPlayername(playername.current.value)
    playername.current.value = ''
  }

  return (
    <section id="player">
      <h2>Welcome {enteredPlayername ?? '2nta meen yad'}</h2>
      <p>
        <input ref={playername} type="text" />
        <button onClick={handlename}>Set Name</button>
      </p>
    </section>
  );
}
