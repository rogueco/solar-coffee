<template>
    <solar-modal>
        <template v-slot:header>
            Receive Shipment
        </template>
        <template v-slot:body>
            <label for="product">Product Received</label>
            <select class="shipmentItems" id="product" v-model="selectedProduct">
                <option disabled value="">Please select one</option>
                <option :key="item.product.id" :value="item" v-for="item in inventory">
                    {{item.product.name}}
                </option>
            </select>
            <label for="qtyReceived">Quantity Received:</label>
            <input id="qtyReceived" type="number" v-model="qtyReceived">
        </template>
        <template v-slot:footer>
            <solar-button
                    @button:click="save"
                    aria-label="save new shipment"
                    type="button"
            >
                Save Received Shipment
            </solar-button>
            <solar-button
                    @button:click="close"
                    aria-label="Close modal"
                    type="button"
            >
                Close
            </solar-button>
        </template>
    </solar-modal>
</template>

<script lang="ts">
    import {Component, Prop, Vue} from "vue-property-decorator";
    import SolarButton from "../SolarButton.vue";
    import SolarModal from "@/components/modals/SolarModal.vue";
    import {IProduct, IProductInventory} from "@/types/Product";
    import {IShipment} from "@/types/Shipment";

    @Component({
        name: "ShipmentModal",
        components: {SolarButton, SolarModal}
    })
    export default class ShipmentModal extends Vue {
        @Prop({required: true, type: Array as () => IProductInventory[]})
        inventory!: IProductInventory[]

        selectedProduct: IProduct = {
            createdOn: new Date(),
            updatedOn: new Date(),
            id: 0,
            description: '',
            isTaxable: false,
            name: "",
            price: 0,
            isArchived: false
        };

        qtyReceived: number = 0;

        close() {
            this.$emit("close");
        }

        save() {
            let shipment: IShipment = {
                productId: this.selectedProduct.id,
                adjustment: this.qtyReceived
            };

            this.$emit('save:shipment', shipment)
        }
    };
</script>

<style scoped></style>
