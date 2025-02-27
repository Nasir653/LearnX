
import { api } from "../utils/AxiosInstance";


interface actionTypes {
  (action: { type: string; message?: string; payload?: any }) : void;
}

const apiInstance =
  (route: string, request: string, success: string, failure: string, formData?: any) =>
  async (dispatch: actionTypes) => {
    try {
    
      let res: { data: { message: string; payload: any } };

      dispatch({ type: request });

      if (formData) {
        res = await api.post(route, formData);
      } else {
        res = await api.get(route);
      }
         console.log("API Response:", res.data); // Debugging
      dispatch({
        type: success,
        message: res.data.message,
        payload: res.data.payload,
      });

     
    } catch (error: any) {
      dispatch({
        type: failure,
        message: error.response.data.message
      });
    }
  };


export const register = (formData: any) => apiInstance("/User/register", "API_REQUEST", "API_SUCCESS", "API_FAILURE", formData);
export const UserLogin = (formData: any) => apiInstance("/User/Login", "API_REQUEST", "API_SUCCESS", "API_FAILURE", formData);
export const fetchUser = () => apiInstance("/User/Fetch/UserData", "API_REQUEST", "API_SUCCESS", "API_FAILURE");
export const CreateNewCourse = (formData: any) => apiInstance("/Course/CreateCourse", "COURSE_API_REQUEST", "COURSE_API_SUCCESS", "COURSE_API_FAILURE", formData);
export const getAllCourses = () => apiInstance("/Course/get/allCourses", "COURSE_API_REQUEST", "COURSE_API_SUCCESS", "COURSE_API_FAILURE");
export const getCourseById = (courseId :any) => apiInstance(`/Course/get/CourseById/${courseId}`, "COURSE_API_REQUEST", "COURSE_API_SUCCESS", "COURSE_API_FAILURE");
export const createLesson = (formData:any , courseId :any) => apiInstance(`/Course/CreateLessons/${courseId}`, "COURSE_API_REQUEST", "COURSE_API_SUCCESS", "COURSE_API_FAILURE", formData);
