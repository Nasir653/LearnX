import { useEffect, useRef } from "react";
import "./TopCategory.css";

const TopCategory = () => {
  const categoriesRef = useRef(null);

  const categories = [
    { name: "Data Science", courses: 68, icon: "📊" },
    { name: "UI/UX Design", courses: 65, icon: "🎨" },
    { name: "Modern Physics", courses: 41, icon: "📚" },
    { name: "Music Production", courses: 89, icon: "🎵" },
    { name: "Biotechnology", courses: 96, icon: "🔬" },
  ];

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add("visible");
          }
        });
      },
      { threshold: 0.2 }
    );

    if (categoriesRef.current) {
      observer.observe(categoriesRef.current);
    }

    return () => observer.disconnect();
  }, []);

  return (
    <section ref={categoriesRef} className="categories-section">
      <h3 className="section-title">Course Categories</h3>
      <h2 className="section-subtitle">
        Browse Top <span className="highlight">Categories</span>
      </h2>
      <div className="categories-grid">
        {categories.map((category, index) => (
          <div key={index} className="category-card">
            <div className="icon">{category.icon}</div>
            <h3>{category.name}</h3>
            <p>{category.courses} Courses</p>
          </div>
        ))}
      </div>
      <button className="view-all-btn">View All Categories</button>
    </section>
  );
};

export default TopCategory;
