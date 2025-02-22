
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


export const register = (formData : any) => apiInstance("/api/User/register" ,"API_REQUEST" , "API_SUCCESS"  ,  "API_FAILURE" , formData)
export const UserLogin = (formData :any) => apiInstance("/api/User/Login" ,"API_REQUEST" , "FETCH_BLOGS_SUCCESS" ,  "API_FAILURE" , formData)