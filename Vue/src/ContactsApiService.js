import axios from "axios";

const client = axios.create({
  baseURL: "https://localhost:5001/api/contact",
  json: true,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
  },
});

export default {
  async execute(method, resource, data) {
    return client({
      method,
      url: resource,
      data,
    }).then((req) => {
      return req.data.data;
    });
  },
  getAll() {
    return this.execute("get", "/");
  },
  create(data) {
    return this.execute("post", "/", data);
  },
  update(data) {
    return this.execute("put", "/", data);
  },
  delete(id) {
    return this.execute("delete", `/${id}`);
  },
};
