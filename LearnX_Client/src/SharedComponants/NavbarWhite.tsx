import { useState } from "react";
import { FaBars, FaTimes, FaSearch } from "react-icons/fa";
import "./NavbarWhite.css";
import { useNavigate } from "react-router-dom";

const NavbarWhite = () => {
  const navigate = useNavigate();
  const [menuOpen, setMenuOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");

  const handleSearch = () => {
    if (searchTerm.trim() !== "") {
      navigate(`/search?q=${searchTerm}`);
    }
  };

  return (
    <nav className="navbar-white">
      <div onClick={() => navigate("/")} className="logo-white">LearnX</div>
      
      {/* Search Bar */}
      <div className="search-bar-white">
        <input 
          type="text" 
          placeholder="Search courses..." 
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button onClick={handleSearch}><FaSearch /></button>
      </div>

      <ul className={`nav-links-white ${menuOpen ? "open" : ""}`}>
        <li onClick={() => { navigate("/"); setMenuOpen(false); }}>Home</li>
        <li>Pages</li>
        <li onClick={() => { navigate("/all/courses"); setMenuOpen(false); }}>Courses</li>
        <li>Blog</li>
        <li>Contact</li>
      </ul>

      <button className="apply-btn-white">Start Free Trial</button>

      <div className="menu-icon-white" onClick={() => setMenuOpen(!menuOpen)}>
        {menuOpen ? <FaTimes /> : <FaBars />}
      </div>
    </nav>
  );
};

export default NavbarWhite;
