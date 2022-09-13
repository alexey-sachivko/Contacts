<template>
  <div class="contacts">
    <div v-if="loading" class="loading">Загрузка...</div>

    <div v-if="contacts" class="content">
      <h1 class="title">Контакты</h1>
      <a class="add-contact" href="#" @click.prevent="toggleCreatePopup"
        >Добавить контакт</a
      >
      <CreatePopup
        v-if="createPopup"
        @close="createPopup = false"
        @createContact="createContact"
      />
      <table>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Номер телефона</th>
            <th>Наименование работы</th>
            <th>Дата рождения</th>
            <th>Действия</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="contact in contacts" :key="contact.id">
            <td>{{ contact.name }}</td>
            <td>{{ contact.mobilePhone }}</td>
            <td>{{ contact.jobTitle }}</td>
            <td>{{ format_date(contact.birthDate) }}</td>
            <td>
              <a href="#" @click.prevent="toogleEditPopup(contact)"
                >Редактировать</a
              >
              -
              <a href="#" @click.prevent="deleteContact(contact.id)">Удалить</a>
            </td>
          </tr>
        </tbody>
      </table>

      <EditPopup
        v-if="editPopup"
        :model="model"
        @close="editPopup = false"
        @updateContact="updateContact"
      />
    </div>
  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
import api from '../ContactsApiService.js';
import EditPopup from "./EditPopup";
import CreatePopup from "./CreatePopup";
import moment from "moment";

export default defineComponent({
    data() {
        return {
            loading: false,
            editPopup: false,
            createPopup: false,
            contacts: null,
            model: {}
        };
    },
    components: {
      EditPopup,
      CreatePopup
    },
    async created() {
        this.getAll()
    },
    methods: {
        format_date(value){
          if (value) {
            return moment(String(value)).format('DD.MM.YYYY')
            }
        },
        async getAll() {
          this.loading = true

          try {
            this.contacts = await api.getAll()
          } finally {
            this.loading = false
          }
        },
        async updateContact(contact) {
          if (this.checkValidation(contact)) {
            await api.update(contact)

            this.model = {}
            this.editPopup = false;

            await this.getAll()
          }
        },
        async createContact(contact) {
          if (this.checkValidation(contact)) {
            await api.create(contact)

            this.model = {}
            this.createPopup = false;

            await this.getAll()
          }
        },
        async deleteContact(id) {
          if (confirm('Вы действительно хотите удалить данный контакт?')) {
            if (this.model.id === id) {
              this.model = {}
            }

            await api.delete(id)
            await this.getAll()
          }
        },
        toogleEditPopup(data) {
          this.editPopup = !this.editPopup;
          this.model = data;
        },
        toggleCreatePopup() {
          this.createPopup = !this.createPopup;
        },
        checkValidation(data) {
          const reNumber = /^(\+375|80)(29|25|44|33|17)(\d{3})(\d{2})(\d{2})$/;
          const reDate = /^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$/;
          if (!data.name) {
            alert("Введите имя");
            return;
          }
          if (!data.mobilePhone.match(reNumber)) {
            alert("Введите корректный номер телефона");
            return;
          }
          if (!data.jobTitle && !data.jobTitle.length < 3) {
            alert("Введите наименование работы");
            return;
          }
          if (!data.birthDate.match(reDate)) {
            alert("Введите корректную дату рождения");
            return;
          }
          return true;
        }
    },
});
</script>

<style scoped>
.contacts {
  max-width: 90rem;
  margin: 0 auto;
}

.content .title {
  font-size: 2rem;
  margin-bottom: 2rem;
}

.add-contact {
  display: block;
  margin-bottom: 2rem;
}

table {
  font-family: "Lucida Sans Unicode", "Lucida Grande", Sans-Serif;
  font-size: 14px;
  border-radius: 10px;
  border-spacing: 0;
  text-align: center;
  width: 100%;
}
th {
  background: #bcebdd;
  color: white;
  text-shadow: 0 1px 1px #2d2020;
  padding: 10px 20px;
}
th,
td {
  border-style: solid;
  border-width: 0 1px 1px 0;
  border-color: white;
}
th:first-child,
td:first-child {
  text-align: left;
}
th:first-child {
  border-top-left-radius: 10px;
}
th:last-child {
  border-top-right-radius: 10px;
  border-right: none;
}
td {
  padding: 10px 20px;
  background: #f8e391;
}
tr:last-child td:first-child {
  border-radius: 0 0 0 10px;
}
tr:last-child td:last-child {
  border-radius: 0 0 10px 0;
}
tr td:last-child {
  border-right: none;
}

.add-contact {
  appearance: none;
  background-color: #2ea44f;
  border: 1px solid rgba(27, 31, 35, 0.15);
  border-radius: 6px;
  box-shadow: rgba(27, 31, 35, 0.1) 0 1px 0;
  box-sizing: border-box;
  color: #fff;
  cursor: pointer;
  display: inline-block;
  font-family: -apple-system, system-ui, "Segoe UI", Helvetica, Arial,
    sans-serif, "Apple Color Emoji", "Segoe UI Emoji";
  font-size: 14px;
  font-weight: 600;
  line-height: 20px;
  padding: 6px 16px;
  position: relative;
  text-align: center;
  text-decoration: none;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  vertical-align: middle;
  white-space: nowrap;
}

.add-contact:focus:not(:focus-visible):not(.focus-visible) {
  box-shadow: none;
  outline: none;
}

.add-contact:hover {
  background-color: #2c974b;
}

.add-contact:active {
  background-color: #298e46;
  box-shadow: rgba(20, 70, 32, 0.2) 0 1px 0 inset;
}
</style>
