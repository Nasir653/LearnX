import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {  getAllCourses, } from "../../redux/Action";
import "./AllCourses.css";
import { motion } from "framer-motion"; // Import Framer Motion for animations
import { useNavigate } from "react-router-dom";

const AllCourses = () => {
  const dispatch = useDispatch();

  const navigate = useNavigate();
  const { payload } = useSelector((state: any) => state.courses);

  useEffect(() => {
    dispatch<any>(getAllCourses());
   
  }, [dispatch]);

  const handleDetails = (CourseId : any)=>{
     
    navigate(`/course/details/${CourseId}`)

   }
  
  return (
    <motion.div
      className="courses-container"
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      transition={{ duration: 1 }}
    >
      <motion.h2
        className="heading"
        initial={{ y: -50, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ duration: 0.8 }}
      >
        All Courses
      </motion.h2>

      {payload && payload.length > 0 ? (
        <motion.div
          className="courses-grid"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 1.2, delay: 0.2 }}
        >
          {payload.map((ele: any, index: number) => (
            <motion.div
              className="course-card"
              key={ele.courseId}
              initial={{ scale: 0.9, opacity: 0 }}
              animate={{ scale: 1, opacity: 1 }}
              transition={{ duration: 0.5, delay: index * 0.1 }}
              whileHover={{ scale: 1.05 }}

              onClick={()=> handleDetails(ele.courseId)}
            >
              <div className="course-image"> <img src={ele.image} alt="" /></div>
              <div className="course-details">
                <h2 className="course-title">{ele.title}</h2>
                <p className="course-description">{ele.description}</p>
                <div className="course-info">
                  <span>⭐ {ele.rating} Rating</span>
                  <span>🧑‍🎓 {ele.totalEnrollments} Enrolled</span>
                </div>
                <p className="course-price">
                  {ele.price > 0 ? `$${ele.price}` : "FREE"}
                </p>
                <motion.a
                  href="#"
                  className="enroll-button"
                  whileHover={{ scale: 1.1 }}
                  whileTap={{ scale: 0.95 }}
                >
                  Enroll Now
                </motion.a>
              </div>
            </motion.div>
          ))}
        </motion.div>
      ) : (
        <motion.p
          className="no-courses"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 1 }}
        >
          No courses available
        </motion.p>
      )}
    </motion.div>
  );
};

export default AllCourses;
