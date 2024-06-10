import React from 'react'
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import DeleteIcon from '@mui/icons-material/Delete';
import FilterListIcon from '@mui/icons-material/FilterList';
import { alpha } from '@mui/material/styles';
import './EnchancedTableToolbar.css';
import { useEffect, useState } from 'react';
import { Button } from '@mui/material';

function EnchancedTableToolbar(props) {

    const [numSelected, setNumSelected] = useState(0);

    useEffect(() => {
      
      setNumSelected(props.numSelected);
    
    }, [props.numSelected]);

    return (
      <Toolbar className='toolbar'
        sx={{
          pl: { sm: 2 },
          pr: { xs: 1, sm: 1 },
          ...(numSelected > 0 && {
            bgcolor: "#f8f8f8",
          }),
        }}
      >
        {numSelected > 0 ? (
          <Typography
            sx={{ flex: '1 1 100%' }}
            color="#221b38"
            variant="subtitle1"
            component="div"
          >
            {numSelected} selected
          </Typography>
        ) : (
          <Typography
            sx={{ flex: '1 1 100%' }}
            variant="h5"
            id="tableTitle"
            component="div"
          >
            Tickets
          </Typography>
        )}
  
        {numSelected > 0 ? (
          <Tooltip title="Delete" className='tooltip' onClick={() => props.deleteRow()}>
            <IconButton>
              <DeleteIcon />
            </IconButton>
          </Tooltip>
        ) : (
          <Tooltip title="Filter list" className='tooltip'>
            <IconButton>
              <FilterListIcon />
            </IconButton>
          </Tooltip>
        )}
      </Toolbar>
    );
}

export default EnchancedTableToolbar
