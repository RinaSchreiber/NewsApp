import React from 'react';
import "../styles/NewsDetails.css"
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';

const NewsDetails = ({ selectedTopic }) => {
  let postToDisplay = selectedTopic;

  if (!postToDisplay || !postToDisplay.title || !postToDisplay.description || !postToDisplay.link) {
    return <div>No posts available</div>;
  }

  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <div id="postTitle">
          <h2>{selectedTopic.title}</h2>
        </div>
        <div id="newsItem">
          <p dangerouslySetInnerHTML={{ __html: postToDisplay.description }} />
        </div>
      </CardContent>
      <div id="link">
        <a href={postToDisplay.link} target="_blank" rel="noopener noreferrer">
          <Button variant="text">
            Read more
          </Button>
        </a>
      </div>
    </Card>
  );
};

export default NewsDetails;









