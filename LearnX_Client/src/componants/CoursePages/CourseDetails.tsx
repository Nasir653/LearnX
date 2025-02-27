import { useDispatch, useSelector } from "react-redux";
import styles from "./CourseDetails.module.css";
import { useEffect, useState } from "react";
import { getCourseById } from "../../redux/Action";
import { useParams } from "react-router-dom";
import CreateLessons from "./CreateLessons";

const CourseDetails = () => {
  const dispatch = useDispatch();
  const { CourseId } = useParams();
  const { payload } = useSelector((state: any) => state.courses);
  const [showLesson, setShowLesson] = useState(false);

  useEffect(() => {
    dispatch<any>(getCourseById(CourseId));
  }, [CourseId, dispatch]);

  return (
    <>
      {!showLesson ? (
        <div className={styles.courseDetailsContainer}>
          {payload ? (
            <div className={styles.courseCard}>
              {payload.image ? (
                <img
                  src={payload.image}
                  alt={payload.title}
                  className={styles.courseImage}
                />
              ) : (
                <p className={styles.noImage}>No Image Available</p>
              )}
              <div className={styles.courseInfo}>
                <h1 className={styles.courseTitle}>{payload.title}</h1>
                <p className={styles.courseDescription}>{payload.description}</p>
                <h3>Category: {payload.category}</h3>
                <h3>Price: ₹{payload.price}</h3>
                <h3>Rating: {payload.rating}</h3>
                <h3>Total Enrollments: {payload.totalEnrollments}</h3>
                <h3>
                  Created At: {new Date(payload.createdAt).toLocaleDateString()}
                </h3>
                <button className={styles.enrollBtn}>Enroll Now</button>
                <button
                  onClick={() => setShowLesson(true)}
                  className={styles.enrollBtn}
                >
                  Create Lessons
                </button>
              </div>
            </div>
          ) : (
            <h2 className={styles.loading}>Loading Course Details...</h2>
          )}
        </div>
      ) : (
        <div className={styles.addLesson}>
          <CreateLessons courseId={payload.courseId} />
        </div>
      )}
    </>
  );
};

export default CourseDetails;
