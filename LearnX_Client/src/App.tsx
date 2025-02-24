import { BrowserRouter, Route, Routes, useLocation } from "react-router-dom";
import HomePage from "./componants/HomePage/HomePage";
import Register from "./componants/UserPages/Register";
import Login from "./componants/UserPages/Login";
import Navbar from "./SharedComponants/Navbar";
import NavbarWhite from "./SharedComponants/NavbarWhite";
import CreateCourse from "./componants/CoursePages/CreateCourse";
import AllCourses from "./componants/CoursePages/AllCourses";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchUser } from "./redux/Action";
import CourseDetails from "./componants/CoursePages/CourseDetails";

const Layout = () => {
  const location = useLocation();

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch<any>(fetchUser());
  })

  return (
    <>
      {location.pathname === "/" ? <Navbar /> : <NavbarWhite />}
      <div className="main">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/user/register" element={<Register />} />
          <Route path="/user/login" element={<Login />} />
          <Route path="/all/courses" element={<AllCourses />} />
          <Route path="/create/NewCourse" element={<CreateCourse />} />
          <Route path="/course/details/:CourseId" element={<CourseDetails />} />
          

        </Routes>
      </div>
    </>
  );
};

function App() {
  return (
    <BrowserRouter>
      <Layout />  {/* This ensures useLocation() works inside BrowserRouter */}
    </BrowserRouter>
  );
}

export default App;
