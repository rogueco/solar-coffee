<template>
    <div class="inventory-container">
        <h1 id="inventoryTitle">Inventory Dashboard</h1>
        <hr/>

        <inventory-chart />

        <div class="inventory-actions">
            <solar-button @button:click="showNewProductModal" id="addNewBtn">
                Add New Item
            </solar-button>
            <solar-button @button:click="showShipmentModal" id="receiveShipmentBtn">
                Receive Shipment
            </solar-button>
        </div>

        <table class="table" id="inventoryTable">
            <tr>
                <th>Item</th>
                <th>Description</th>
                <th>Quantity On-hand</th>
                <th>Unit Price</th>
                <th>Taxable</th>
                <th>Delete</th>
            </tr>
            <tr :key="item.id" v-for="item in inventory">
                <td>
                    {{ item.product.name }}
                </td>
                <td>
                    {{item.product.description}}
                </td>
                <td
                        v-bind:class="`${applyColor(item.quantityOnHand, item.idealQuantity)}`"
                >
                    {{ item.quantityOnHand }}
                </td>
                <td>
                    {{ item.product.price | price }}
                </td>
                <td>
                    {{ item.product.isTaxable ? "Yes" : "No" }}
                </td>
                <td>
                    <div
                            @click="archiveProduct(item.product.id)"
                            class="lni lni-cross-circle product-archive"
                    ></div>
                </td>
            </tr>
        </table>

        <new-product-modal
                @close="closeModals"
                @save:product="saveNewProduct"
                v-if="isNewProductVisible"
        />
        <shipment-modal
                :inventory="inventory"
                @close="closeModals"
                @save:shipment="saveNewShipment"
                v-if="isShipmentVisible"
        />
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from "vue-property-decorator";
    import {IProduct, IProductInventory} from "@/types/Product";
    import SolarButton from "@/components/SolarButton.vue";
    import ShipmentModal from "@/components/modals/ShipmentModal.vue";
    import NewProductModal from "@/components/modals/NewProductModal.vue";
    import {IShipment} from "@/types/Shipment";
    import {InventoryService} from "@/services/inventory-service";
    import {ProductService} from "@/services/product-service";
    import InventoryChart from "@/components/charts/InventoryChart.vue";

    const inventoryService = new InventoryService();
    const productService = new ProductService();

    @Component({
        name: "Inventory",
        components: {SolarButton, ShipmentModal, NewProductModal, InventoryChart}
    })
    export default class Inventory extends Vue {
        isNewProductVisible: boolean = false;
        isShipmentVisible: boolean = false;

        inventory: IProductInventory[] = [];

        async archiveProduct(productId: number) {
            await productService.archive(productId);
            await this.initialize();
        }

        async saveNewProduct(newProduct: IProduct) {
            await productService.save(newProduct);
            this.isNewProductVisible = false;
            await this.initialize();
        }

        applyColor(currentInventoryLevel: number, idealInventoryLevel: number) {
            if (currentInventoryLevel <= 0) {
                return "red";
            } else if (Math.abs(idealInventoryLevel - currentInventoryLevel) > 8) {
                return "yellow"
            }

            return "green"
        }

        closeModals() {
            this.isNewProductVisible = false;
            this.isShipmentVisible = false;
        }

        showNewProductModal() {
            this.isNewProductVisible = true;
        }

        showShipmentModal() {
            this.isShipmentVisible = true;
        }

        async saveNewShipment(shipment: IShipment) {
            await inventoryService.updatedInventoryQuantity(shipment);
            this.isShipmentVisible = false;
            await this.initialize();
        }

        async initialize() {
            this.inventory = await inventoryService.getInventory();
            await this.$store.dispatch("assignSnapshots");
        }

        async created() {
            await this.initialize();
        }
    }
</script>

<style lang="scss" scoped>
    @import "@/scss/global.scss";

    .green {
        font-weight: bold;
        color: $solar-green;
    }

    .yellow {
        font-weight: bold;
        color: $solar-yellow;
    }

    .red {
        font-weight: bold;
        color: $solar-red;
    }

    .inventory-actions {
        display: flex;
        margin-bottom: 0.8rem;
    }

    .product-archive {
        cursor: pointer;
        font-weight: bold;
        font-size: 1.2rem;
        color: $solar-red;
    }
</style>
