import { useEffect, useState } from "react";
import "./AchivementsPage.css";

const AchievementsPage = () => {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const onScroll = () => {
      const section = document.querySelector(".achievements-section");
      if (section) {
        const sectionTop = section.getBoundingClientRect().top;
        if (sectionTop < window.innerHeight * 0.75) {
          setVisible(true);
        }
      }
    };

    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  const achievements = [
    { icon: "👨‍🏫", number: 82, label: "Enrolled Students", color: "#ff6a6a" },
    { icon: "📖", number: 460, label: "Academic Programs", color: "#8e44ad" },
    { icon: "👨‍🎓", number: 20, label: "Certified Students", color: "#f39c12" },
    { icon: "🏆", number: 200, label: "Award Winning", color: "#1abc9c" },
  ];

  return (
    <section className={`achievements-section ${visible ? "visible" : ""}`}>
      <p className="sub-text">Some Fun Fact</p>
      <h2 className="title">
        Our Great <span className="highlight">Achievement</span>
      </h2>

      <div className="achievements-grid">
        {achievements.map((item, index) => (
          <div key={index} className="achievement-card">
            <div
              className="icon"
              style={{ backgroundColor: item.color }}
            >
              {item.icon}
            </div>
            <h3>{item.number}</h3>
            <p>{item.label}</p>
          </div>
        ))}
      </div>
    </section>
  );
};

export default AchievementsPage;
