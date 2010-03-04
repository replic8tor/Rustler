using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class WitherHackDecorationRequest
        : RequestObject
    {
        private int _itemId;

        public int ItemId
        {
          get { return _itemId; }
          set { _itemId = value; }
        }
        private int _x;

        public int X
        {
          get { return _x; }
          set { _x = value; }
        }
        private int _y;

        public int Y
        {
          get { return _y; }
          set { _y = value; }
        }

        private string _plantRequest;

        public string PlantRequest
        {
          get { return _plantRequest; }
          set { _plantRequest = value; }
        }

         /*
                     *   $amf->_bodys[0]->_value[1][0]['params'][1]['id'] = $idu;
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['className'] = 'Decoration';
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['position']['x'] = $x;
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['position']['y'] = $y;
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['position']['z'] = '0';
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['deleted'] = false;
                    	 $amf->_bodys[0]->_value[1][0]['params'][1]['itemName'] = 'watermelonyellow';
                    	 $amf->_bodys[0]->_value[1][0]['params'][2][0]['isStorageWithdrawal'] = false;
                    	 $amf->_bodys[0]->_value[1][0]['params'][2][0]['isGift'] = false;
                     * */
                
        public override object[] GetParameterArray()
        {
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", this._itemId);
            parameter1.Add("className","Decoration");
            Classes.ObjectPosition pos = new Classes.ObjectPosition(){ X = _x, Y = _y, Z = 0 };
            parameter1.Add("position", pos.ToObject());
            parameter1.Add("deleted", false);
            parameter1.Add("itemName", _plantRequest);
          

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("isStorageWithdrawal", false);
            parameter2.Add("isGift", false);
            return new object[]{ "place", parameter1, new object[] { parameter2 } };
        }



        public WitherHackDecorationRequest(int sequence, int itemId, int x, int y, string plantName)
            : base(sequence, "WorldService.performAction")
        {
            _itemId = itemId;
            _x = x;
            _y = y;
            PlantRequest = plantName;
        }
    }
}
