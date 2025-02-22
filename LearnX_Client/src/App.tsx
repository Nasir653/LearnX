import { BrowserRouter, Route, Routes } from "react-router-dom"
import Register from "./componants/UserPages/Register"
import Login from "./componants/UserPages/Login"


function App() {


  return (
    <>

      <BrowserRouter>
      
      
        <div className="main">

          <Routes>
            
            <Route path="/user/register" element={<Register/>} />
            <Route path="/user/login" element={<Login/>} />

          </Routes>


          


        </div>
      
      
      
      
      </BrowserRouter>
     
    </>
  )
}

export default App
