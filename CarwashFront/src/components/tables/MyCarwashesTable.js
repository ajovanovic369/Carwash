import React, { useEffect, useState } from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import avatar from "../../assets/avatar.webp";
import axios from "axios";

export default function DynamicTable() {
  const [data, setData] = useState([]);
  const token = localStorage.getItem("token");

  useEffect(() => {
    axios
      .get("https://localhost:7090/api/carwashes/mycarwashes",
        {
          mode: "cors",
          method: "GET",
          withCredentials: true,
          credentials: "include",
          headers: {
            'Content-type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        }
      )
      .then((res) => {
        setData(res.data);
        console.log("Result:", data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <TableContainer component={Paper}>
      <Table aria-label="simple table" stickyHeader>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell align="right">Picture</TableCell>
            <TableCell align="right">CarwashId</TableCell>
            <TableCell align="right">address</TableCell>
            <TableCell align="right">openingHours</TableCell>
            <TableCell align="right">closingHours</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id}>
              <TableCell component="th" scope="row">
                {row.name}
              </TableCell>
              <TableCell align="right"><img src={avatar} width={60}></img></TableCell>
              <TableCell align="right">{row.id}</TableCell>
              <TableCell align="right">{row.address}</TableCell>
              <TableCell align="right">{row.openingHours}</TableCell>
              <TableCell align="right">{row.closingHours}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}