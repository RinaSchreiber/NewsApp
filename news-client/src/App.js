import React, { useState, useEffect } from 'react';
import axios from 'axios';
import NewsList from './components/NewsList';
import NewsDetails from './components/NewsDetails';
import './App.css';

axios.defaults.baseURL = process.env.REACT_APP_BASE_URL;

const App = () => {
  const [news, setNews] = useState([]);
  const [selectedTopic, setSelectedTopic] = useState(null);


  useEffect(() => {
    const fetchNews = async () => {
      try {
        const response = await axios.get('/news');
        setNews(response.data);
        setSelectedTopic(response.data[0]);
      } catch (error) {
        console.error('Error fetching news:', error);
      }
    };

    fetchNews();
  }, []);

  const handleTopicSelect = (topic) => {
    setSelectedTopic(topic);
  };

  return (
    <div id="app">
      <header>
        <h1>News App</h1>
      </header>
      <div id="content">
        <NewsList news={news} onTopicSelect={handleTopicSelect} />
        <NewsDetails selectedTopic={selectedTopic} />
      </div>
    </div>
  );
};

export default App;