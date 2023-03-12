<template>
        <span><i class="bi bi-clock"></i> {{ fromNow(date) }}</span>

</template>

<script>
import moment from 'moment'
export default {
  name: 'DateFromNow',
  props: {
    date: String
  },
  methods: {
    fromNow (date) {
      return moment(date).fromNow()
    }
  }
}
</script>
