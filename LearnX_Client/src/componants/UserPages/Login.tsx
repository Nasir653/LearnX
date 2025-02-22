import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { UserLogin } from '../../redux/Action';
import './Login.css';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const Login = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { message } = useSelector((state: any) => state.user); 
  const [formData, setFormData] = useState({
    Email: "",
    Password: ""
  });

  const handleData = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    await dispatch<any>(UserLogin(formData));
    toast.success(message);
  };

  return (

    <div className="container-fluid login-main">
    <div className="login-container">
      

      <div className="login-left">
        <div className="login-logo">
          <img src="/assets/logo.png" alt="Spacer Logo" />
        </div>
        <h2>Welcome to</h2>
        <h1>Spacer</h1>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
        </p>
        <div className="footer-links">
          <a href="#">Creator Here</a>
          <a href="#">Designer Here</a>
        </div>
      </div>

      {/* Right Side - Login Form */}
      <div className="login-right">
        <div className="form-container">
          <h2>Login Your Account</h2>
          <form onSubmit={handleLogin}>
             
            <input
              type="email"
              placeholder="Email"
              required
              name="Email"
              value={formData.Email}
              onChange={handleData}
              className="login-input"
            />
            <input
              type="password"
              placeholder="Password"
              required
              name="Password"
              value={formData.Password}
              onChange={handleData}
              className="login-input"
            />
            
            <div className="terms">
              <input type="checkbox" required />
              <span>By signing up, I agree to <a href="#">Terms & Conditions</a></span>
            </div>

            <button type="submit" className="sign-up-button">Sign In</button>
            <button className="sign-in-button" onClick={()=> navigate("/user/register")}>Sign Up</button>
          </form>
        </div>
      </div>

      </div>
      
      </div>
  );
};

export default Login;
