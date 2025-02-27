import { useState } from "react";
import "./CreateLessons.css";
import { useDispatch } from "react-redux";
import { createLesson } from "../../redux/Action";

const CreateLessons = ({ courseId }: any) => {
  
  const dispatch = useDispatch();
  const [lesson, setLesson] = useState({
    title: "",
    videoFile: null,
    duration: "",
  
  });


  
  const handleChange = (e :any) => {
    const { name, type, files, value } = e.target;
    setLesson({
      ...lesson,
      [name]: type === "file" ? files[0] : value
    });
  };

  console.log(lesson);
  
  

 const handleSubmit = (e: any) => {
  e.preventDefault();
  const formData = new FormData();
  formData.append("Title", lesson.title); // Capital T se Title
  formData.append("Duration", lesson.duration.toString()); // .toString() daal warna ASP.NET goli marta hai
  
  if (lesson.videoFile) {
    formData.append("videoFile", lesson.videoFile);
  }

  console.log("FormData: ", formData);

  dispatch<any>(createLesson(formData, courseId));
};


  return (
    <div className="lesson-form-container">
      <form onSubmit={(e)=>handleSubmit(e)} className="lesson-form">
        <h2 className="lesson-title">Create Lesson</h2> 
        <div className="form-group">
          <label>Title:</label>
          <input type="text" name="title" value={lesson.title} onChange={handleChange} required />
        </div>
        <div className="form-group">
          <label>Video File:</label>
          <input type="file" name="videoFile" accept="video/*" onChange={handleChange} required />
        </div>
        <div className="form-group">
          <label>Duration (minutes):</label>
          <input type="number" name="duration" value={lesson.duration} onChange={handleChange} required />
        </div>
        <button type="submit" className="submit-btn">Create Lesson</button>
      </form>
    </div>
  );
};

export default CreateLessons;
