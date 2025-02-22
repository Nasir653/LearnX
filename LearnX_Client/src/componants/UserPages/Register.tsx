import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { register } from "../../redux/Action"; 
import "./Register.css";
import { toast } from "react-toastify";


const Register = () => {
  const dispatch = useDispatch();
  const { loading, message } = useSelector((state: any) => state.user); 

  const [formData, setFormData] = useState({
    Username: "",
    Email: "",
    Password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await dispatch<any>(register(formData)); 
   toast.success(message);
 
  };

  return (

    <div className="container-fuild">


    <div className="register-container">
      
     
      <div className="register-left">
        <div className="register-logo">
          <img src="/assets/logo.png" alt="Spacer Logo" />
        </div>
        <h2>Welcome to</h2>
        <h1>Spacer</h1>
        <p>
          Join the community today! Sign up to access premium features and stay connected.
        </p>
        <div className="footer-links">
          <a href="#">Privacy Policy</a>
          <a href="#">Help & Support</a>
        </div>
      </div>

      {/* Right Side - Register Form */}
      <div className="register-right">
        <div className="form-container">
          <h2>Join Us Today</h2>
          <form onSubmit={handleSubmit}>
            <input
              type="text"
              placeholder="Username"
              required
              name="Username"
              value={formData.Username}
              onChange={handleChange}
              className="register-input"
            />
            <input
              type="email"
              placeholder="Email"
              required
              name="Email"
              value={formData.Email}
              onChange={handleChange}
              className="register-input"
            />
            <input
              type="password"
              placeholder="Create Password"
              required
              name="Password"
              value={formData.Password}
              onChange={handleChange}
              className="register-input"
            />

            <div className="terms">
              <input type="checkbox" required />
              <span>By signing up, I agree to the <a href="#">Terms & Conditions</a></span>
            </div>

            <button type="submit" className="register-button">
              {loading ? "Registering..." : "Register"}
            </button>

            <div className="already-account">
              <p>Already have an account? <a href="/user/login">Login</a></p>
            </div>
          </form>
        </div>
      </div>

        </div>
    </div>
  );
};

export default Register;
