import React from 'react';
import "../styles/NewsList.css"
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItem';


const NewsList = ({ news, onTopicSelect }) => {
    return (
        <div id="newsList">
            <h2 variant="h2" gutterBottom>Topics</h2>
            {
                news.map((item) => (

                    <ListItem button component="a"className="item in list" target="_blank" onClick={() => onTopicSelect(item)}>
                      {item.title} 
                    </ListItem>
                ))}
            
        </div>
    );
};

export default NewsList;