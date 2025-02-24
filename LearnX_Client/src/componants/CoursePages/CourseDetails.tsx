
import { useDispatch } from "react-redux";
import "./CourseDetails.css";
import { useEffect } from "react";
import { getCourseById } from "../../redux/Action";
import { useParams } from "react-router-dom";
const CourseDetails = () => {

  const dispatch = useDispatch();
  const {CourseId} = useParams();


  useEffect(() => {
    console.log(CourseId);
    
    dispatch<any>(getCourseById(CourseId))
  }, []);
  return (
    <div className="aaa">CourseDetails</div>
  )
}

export default CourseDetails