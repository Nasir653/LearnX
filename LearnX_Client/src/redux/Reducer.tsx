import { createReducer } from "@reduxjs/toolkit";

interface stateTypes {

  loading : boolean,
  message : string,
  payload : any
  blogs : any
}

const userIntialState : stateTypes= { 
  loading: false,
  message: "",
  payload: {},
  blogs : []
};

export const userReducer = createReducer(userIntialState, (builder : any): void => {
  builder
    .addCase("API_REQUEST", (state : stateTypes) => {
      state.loading = true;
    })
    .addCase("API_SUCCESS" ,(state : stateTypes , action : stateTypes)=>{
      state.loading = false
      state.message = action.message
      state.payload = action.payload;
      
      
    })
    .addCase("FETCH_BLOGS_SUCCESS" ,(state : stateTypes , action : stateTypes)=>{
      state.loading = false
      state.message = action.message
      state.blogs = action.payload;
    })
    .addCase("API_FAILURE", (state :stateTypes,  action : stateTypes) => {
      state.loading = false;
      state.message = action.message;
    })
    .addCase("CLEAR_MESSAGE" , (state :stateTypes)=>{

      state.message = ""

    })
});