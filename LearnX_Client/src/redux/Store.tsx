import { configureStore } from "@reduxjs/toolkit";
import { CourseReducer, userReducer } from "./Reducer";

const store = configureStore  ({
  reducer: {
    user: userReducer,
    courses : CourseReducer,
  },
});


export type AppDispatch = typeof store.dispatch;

export default store;