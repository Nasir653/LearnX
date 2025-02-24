import { useEffect, useState } from "react";
import "./HomePage.css";
import TopCategory from "./TopCategory";
import AchievementsPage from "./AchivementsPage";


const images = [
  "https://cloudinary.hbs.edu/hbsit/image/upload/s--BAROcXag--/f_auto,c_fill,h_375,w_750,/v20200101/36F24CBCC6EFE6072D17BF11948E162F.jpg",
  "https://online.njit.edu/sites/online/files/iStock-509114480.jpg",
  "https://thelachatupdate.com/wp-content/uploads/2015/12/crop380w_istock_000002193842xsmall-books.jpg",
];

const buttonTexts = [
  { learnMore: "Learn More", courses: "Our Courses" },
  { learnMore: "Start Now", courses: "Explore Courses" },
  { learnMore: "Join Today", courses: "Browse Courses" },
];

const HomePage = () => {



  const [currentImage, setCurrentImage] = useState(0);




  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImage((prev) => (prev + 1) % images.length);
    }, 4000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className="homepage_main">
    

      {/* Hero Section */}
      <section className="hero-section">
        <div className="hero-content">
          <h1>
            Classical <span className="highlight">Education</span> For The Future
          </h1>
          <p className="subtext">
            It is a long-established fact that readers are distracted by readable content.
          </p>
          <div className="hero-buttons">
            <button className="btn btn-primary">{buttonTexts[currentImage].learnMore}</button>
            <button className="btn btn-secondary">{buttonTexts[currentImage].courses}</button>
          </div>
        </div>
        <div className="hero-image">
          <img src={images[currentImage]} alt="Hero" />
        </div>
      </section>

      {/* Course Categories Section */}
    <section>  <TopCategory/>  </section>
    <section>  <AchievementsPage/>  </section>

    </div>
  );
};

export default HomePage;
