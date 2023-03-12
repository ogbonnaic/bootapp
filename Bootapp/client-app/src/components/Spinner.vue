<template>
<div class="d-flex justify-content-center">
<div class="spinner-border text-warning" role="status">

</div>
<span style="margin-left:10px;" class="align-middle">{{ msg }}</span>
        </div>
</template>
<script>
export default {
  name: 'Spinner',
  props: {
    msg: String
  }
}
</script>
