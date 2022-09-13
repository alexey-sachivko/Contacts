<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <h2>Редактирование контакта #{{ contact.id }}</h2>
          </div>

          <div class="modal-body">
            <form>
              <div>
                <label for="name">Имя:</label>
                <input name="name" type="text" v-model="contact.name" />
              </div>
              <div>
                <label for="mobilePhone">Номер телефона:</label>
                <input
                  name="mobilePhone"
                  type="text"
                  v-model="contact.mobilePhone"
                />
              </div>
              <div>
                <label for="jobTitle">Наименование работы:</label>
                <input name="jobTitle" type="text" v-model="contact.jobTitle" />
              </div>
              <div>
                <label for="birthDate">Дата рождения:</label>
                <input
                  name="birthDate"
                  type="date"
                  v-model="contact.birthDate"
                />
              </div>
            </form>
          </div>

          <div class="modal-footer">
            <button
              class="modal-default-button"
              @click="$emit('updateContact', contact)"
            >
              Обновить
            </button>
            <button class="modal-default-button" @click="$emit('close')">
              Закрыть
            </button>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
export default {
  data() {
    return {
      contact: {
        id: this.model.id,
        name: this.model.name,
        mobilePhone: this.model.mobilePhone,
        jobTitle: this.model.jobTitle,
        birthDate: this.model.birthDate.split("T")[0],
      },
    };
  },
  props: {
    model: Object,
  },
  emits: ["close", "updateContact"],
};
</script>

<style scoped>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 500px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}

.modal-default-button {
  float: right;
}

.modal-enter {
  opacity: 0;
}

.modal-leave-active {
  opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}

form div {
  display: flex;
  gap: 0.3rem;
  align-items: center;
}

form div:not(:last-child) {
  margin-bottom: 1rem;
}
</style>
