import { useEffect, useState } from "react";
import { FaBars, FaTimes } from "react-icons/fa";
import "./Navbar.css";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";

const Navbar = () => {

  const navigate = useNavigate();
   const { payload } = useSelector((state: any) => state.user);

  const [menuOpen, setMenuOpen] = useState(false);
  const [scrolled, setScrolled] = useState(false);
  
    useEffect(() => {
      const handleScroll = () => {
        setScrolled(window.scrollY > 50);
      };
      window.addEventListener("scroll", handleScroll);
      return () => window.removeEventListener("scroll", handleScroll);
    }, []);
  return (
    <div>

        {/* Navbar */}
      <nav className={`navbar ${scrolled ? "scrolled" : ""}`}>
        <div onClick={()=> navigate("/")} className="logo">LearnX</div>
        <ul className={`nav-links ${menuOpen ? "open" : ""}`}>
          <li onClick={()=> navigate("/")}>Home</li>
          <li>Pages</li>
          <li onClick={()=> navigate("/all/courses")}>Courses</li>
          <li>Blog</li>
          <li>Contact</li>
          <li onClick={()=> navigate("/create/NewCourse")}>Create Course</li>
          <li>{payload.username}</li>
        </ul>
        <button className="apply-btn">Start Free Trial</button>
        <div className="menu-icon" onClick={() => setMenuOpen(!menuOpen)}>
          {menuOpen ? <FaTimes /> : <FaBars />}
        </div>
      </nav>


    </div>
  )
}

export default Navbar