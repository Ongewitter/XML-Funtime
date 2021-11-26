namespace ClassLibrary {
    public class Order {
        public Boolean status; //XML serialisation needs public properties

        public void SetStatus(Boolean status) {
            this.status = status;
        }
        public Boolean GetStatus() {
            return this.status;
        }
        public override string ToString() {
            return "status: " + status;
        }
    }
}
