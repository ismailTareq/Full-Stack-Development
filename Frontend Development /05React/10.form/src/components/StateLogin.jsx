import { useState } from "react";

export default function StateLogin() {
  // const [enteredemail,setenteredemail] = useState('')
  // const [enteredpass,setenteredpass] = useState('')
  const [enteredvalue,setenteredvalue] = useState({
    email:'',
    password:''
  })

  const [didEdit,setdidEdit] = useState({
    email: false,
    password: false
  })

  const EmailIsInvalid =didEdit.email && !enteredvalue.email.includes('@')

  function handleSubmit(event){
    event.preventDefault()
    console.log(enteredvalue)
  }

  function handleInputChanges(identifeir, event){
    setenteredvalue(prevstate => ({
      ...prevstate,
      [identifeir]: event.target.value
    }))
    setdidEdit((prevedit) => ({
      ...prevedit,
      [identifeir]:false
    }))
  }
  function handleInputBlur(identifier){
    setdidEdit((prevvalue)=>({
      ...prevvalue,
      [identifier]:true
    }))
  }
  
  return (
    <form onSubmit={handleSubmit}>
      <h2>Login</h2>

      <div className="control-row">
        <div className="control no-margin">
          <label htmlFor="email">Email</label>
          <input id="email" type="email" name="email"
          onBlur={()=>{handleInputBlur('email')}} 
          onChange={(e)=>handleInputChanges('email',e)} value={enteredvalue.email}/>

          <div className="control-error">
            {EmailIsInvalid && <p>please enter a proper email</p>}
          </div>
        </div>

        <div className="control no-margin">
          <label htmlFor="password">Password</label>
          <input id="password" type="password" name="password" 
          onChange={(e)=>handleInputChanges('password',e)} value={enteredvalue.password} />
        </div>
      </div>

      <p className="form-actions">
        <button className="button button-flat">Reset</button>
        <button className="button">Login</button>
      </p>
    </form>
  );
}
