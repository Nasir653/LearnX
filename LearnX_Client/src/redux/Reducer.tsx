import { createReducer } from "@reduxjs/toolkit";

interface stateTypes {

  loading : boolean,
  message : string,
  payload : any
 
}

const IntialState : stateTypes= { 
  loading: false,
  message: "",
  payload: {},

};

export const userReducer = createReducer(IntialState, (builder : any): void => {
  builder
    .addCase("API_REQUEST", (state : stateTypes) => {
      state.loading = true;
    })
    .addCase("API_SUCCESS" ,(state : stateTypes , action : stateTypes)=>{
      state.loading = false
      state.message = action.message
      state.payload = action.payload;
      console.log(action);
        localStorage.setItem("userPayload", JSON.stringify(action.payload));
      
    })
    .addCase("API_FAILURE", (state :stateTypes,  action : stateTypes) => {
      state.loading = false;
      state.message = action.message;                                                       
    })
    .addCase("CLEAR_MESSAGE" , (state :stateTypes)=>{

      state.message = ""

    })
});

interface CourseAction {

  loading : boolean,
  message : string,
  payload : any
 
   }

          

export const CourseReducer = createReducer(IntialState, (builder :any) => {
  builder
    .addCase("COURSE_API_REQUEST", (state : stateTypes) => {
      state.loading = true;
    })
    .addCase("COURSE_API_SUCCESS", (state :stateTypes, action : CourseAction ) => {
      state.loading = false;
      state.message = action.message;
      state.payload = action.payload;
    })
    .addCase("COURSE_API_FAILURE", (state : stateTypes, action : stateTypes) => {
      state.loading = false;
      state.message = action.message;
    })
    .addCase("CLEAR_MESSAGE", (state : stateTypes) => {
      state.message = "";
    });
});
 