import React, { useState } from "react";
import "./CreateCourse.css";
import { motion } from "framer-motion";
import { useDispatch } from "react-redux";
import { CreateNewCourse } from "../../redux/Action";


const CreateCourse = () => {
  const dispatch = useDispatch();

  const [formData, setFormData] = useState({
    title: "",
    description: "",
    price: "",
    category: "",
      level: "",

    
  });

  const [Images, setImage] = useState<File | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prevInput) => ({ ...prevInput, [name]: value }));
  };

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      setImage(e.target.files[0]);
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    const formDataToSend = new FormData();
    formDataToSend.append("title", formData.title);
    formDataToSend.append("description", formData.description);
    formDataToSend.append("price", formData.price);
    formDataToSend.append("category", formData.category);
    formDataToSend.append("level", formData.level);
    if (Images) {
      formDataToSend.append("Images", Images);
    }

    dispatch<any>(CreateNewCourse(formDataToSend)); // Dispatching the action

    // Reset form
    // setFormData({
    //   title: "",
    //   description: "",
    //   price: ""
    //   category: "",
    //   level: "",
    // });
    // setImage(null);
  };

  return (
    <motion.div
      className="create-course-container"
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      transition={{ duration: 0.8 }}
    >
      <motion.h2
        className="title"
        initial={{ y: -20, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ duration: 0.6 }}
      >
        Create New Course
      </motion.h2>

      <motion.form
        className="course-form"
        onSubmit={handleSubmit}
        initial={{ opacity: 0, scale: 0.9 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
        encType="multipart/form-data"
      >
        <input type="text" placeholder="Title" name="title" value={formData.title} onChange={handleChange} required />

        <textarea
          placeholder="Description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          required
        />

        <input type="number" placeholder="Price" name="price" value={formData.price} onChange={handleChange} required />

        <input type="file" accept="image/*" onChange={handleImageChange} required />

        <select name="category" value={formData.category} onChange={handleChange} required>
          <option value="">Select Category</option>
          <option value="Web Development">Web Development</option>
          <option value="Data Science">Data Science</option>
          <option value="AI & ML">AI & ML</option>
          <option value="Business">Business</option>
        </select>

        <select name="level" value={formData.level} onChange={handleChange} required>
          <option value="">Select Level</option>
          <option value="Beginner">Beginner</option>
          <option value="Intermediate">Intermediate</option>
          <option value="Advanced">Advanced</option>
        </select>

        {Images && (
          <motion.div className="preview" initial={{ opacity: 0 }} animate={{ opacity: 1 }} transition={{ duration: 0.5 }}>
            <img src={URL.createObjectURL(Images)} alt="Preview" />
          </motion.div>
        )}

        <motion.button
          type="submit"
          className="submit-button"
          whileHover={{ scale: 1.05 }}
          whileTap={{ scale: 0.95 }}
        >
          Create Course
        </motion.button>
      </motion.form>
    </motion.div>
  );
};

export default CreateCourse;
