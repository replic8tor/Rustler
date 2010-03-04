using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class WitherHackClearRequest
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
        * 	 $amf->_bodys[0]->_value[1][0]['params'][1]['id'] = $id;
            $amf->_bodys[0]->_value[1][0]['params'][1]['plantTime'] = '0';
            $amf->_bodys[0]->_value[1][0]['params'][1]['className'] = 'Plot';
            $amf->_bodys[0]->_value[1][0]['params'][1]['isJumbo'] = false;
            $amf->_bodys[0]->_value[1][0]['params'][1]['state'] = 'withered';
            $amf->_bodys[0]->_value[1][0]['params'][1]['direction'] = '0';
            $amf->_bodys[0]->_value[1][0]['params'][1]['isBigPlot'] = false;
            $amf->_bodys[0]->_value[1][0]['params'][1]['position']['x'] = $x;
            $amf->_bodys[0]->_value[1][0]['params'][1]['position']['y'] = $y;
            $amf->_bodys[0]->_value[1][0]['params'][1]['position']['z'] = '0';
            $amf->_bodys[0]->_value[1][0]['params'][1]['deleted'] = false;
            $amf->_bodys[0]->_value[1][0]['params'][1]['itemName'] = 'watermelonyellow';
            $amf->_bodys[0]->_value[1][0]['params'][1]['isProduceItem'] = false;
            $amf->_bodys[0]->_value[1][0]['params'][2] = array();
        */
        public override object[] GetParameterArray()
        {
            Classes.ObjectPosition pos = new Classes.ObjectPosition() { X = _x, Y = _y, Z = 0 };
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", _itemId);
            parameter1.Add("state", "withered");
            parameter1.Add("itemName", _plantRequest);
            parameter1.Add("plantTime", (float)0);
            parameter1.Add("direction", 0);
            parameter1.Add("isJumbo", false);
            parameter1.Add("position", pos.ToObject());
            parameter1.Add("isBigPlot", false);
            parameter1.Add("className", "Plot");
            parameter1.Add("deleted", false);
            parameter1.Add("isProduceItem", false);

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("energyCost", 0);
            return new object[] { "clear", parameter1, new object[] { parameter2 } };
        }

        public WitherHackClearRequest(int sequence, int itemId, int x, int y, string plantRequest)
            : base(sequence, "WorldService.performAction")
        {
            _itemId = itemId;
            _x = x;
            _y = y;
            _plantRequest = plantRequest;
        }


    }
}
