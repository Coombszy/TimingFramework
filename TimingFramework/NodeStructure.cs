using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace TimingFramework
{
    public class NodeStructure
    {
        private Node FirstNode;
        public NodeStructure(List<object> data)
        {
            FirstNode = new Node(Serialize(data[0]));
            data.RemoveAt(0);
            foreach(object item in data)
            {
                FirstNode.AddData(Serialize(item));
            }
        }

        public object Index(int index)
        {
            string SerializedData = FirstNode.Indexing(index).String;
            if (SerializedData == "")
            {
                return "";
            }
            else
            {
                return (object)Deserialize(SerializedData);
            }
        }
        public bool Contains(object DoesExist)
        {
            string data = Serialize(DoesExist);
            if (data == "")
            { 
                return false;
            }
            else
            {
                if (FirstNode == null)
                {
                    return false;
                }
                else
                {
                    return FirstNode.Contains(data);
                }
            }

        }

        private object Deserialize(string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(object));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (object)xmlSerializer.Deserialize(textReader);
            }
        }
        private string Serialize(object toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(object));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }
    class Node
    {
        //Node Data
        public char MyData = '0';
        public int Quantity = 0;
        public List<object> ChildNodes = new List<object>();
        public bool Terminator = false;
        //Custom struct for returning multiple values
        public struct StringIndex
        {
            public string String;
            public int index;
            public StringIndex(string AString, int AIndex)
            {
                String = AString;
                index = AIndex;
            }
        }
        //Node Methods and Constructors
        public Node(string data)
        {
            this.MyData = data[0];
            data = data.Substring(1);
            if(data != "")
            {
                int index = DoesChildContainThisData(data[0]);
                if (index == -1)
                {
                    ChildNodes.Add(new Node(data));
                }
                else
                {
                    ((Node)ChildNodes[index]).AddData(data);
                }
            }
            else
            {
                this.Quantity += 1;
                this.Terminator = true;
            }

        }
        public void AddData(string data)
        {
            data = data.Substring(1);
            if (data != "")
            {
                int index = DoesChildContainThisData(data[0]);
                if (index == -1)
                {
                    ChildNodes.Add(new Node(data));
                }
                else
                {
                    ((Node)ChildNodes[index]).AddData(data);
                }
            }
            else
            {
                this.Quantity += 1;
            }
        }
        public StringIndex Indexing(int index)
        {
            string toReturn = @"";
            foreach(Node Child in ChildNodes)
            {
                if (Child.Terminator == true && index == 0)
                {
                    toReturn += @""+(char)MyData+(char)Child.MyData;
                    break;
                }
                else if (Child.Terminator == true)
                {
                    index--;
                }
                StringIndex data = Child.Indexing(index);
                index = data.index;
                if (data.String != "")
                {
                    return new StringIndex(MyData + data.String, 0);
                }
            }
            return new StringIndex(toReturn, index);
        }
        public bool Contains(string data)
        {
            data = data.Remove(0, 1);
            if (data == "")
            {
                return Terminator;
            }
            else
            {

                int index = DoesChildContainThisData(data[0]);
                if (index != -1)
                {
                    return ((Node)ChildNodes[index]).Contains(data);
                }
                else
                {
                    return false;
                }

            }
        }
        private int DoesChildContainThisData(char dataSnip)
        {
            foreach(Node ChildNode in ChildNodes)
            {
                if(ChildNode.MyData == dataSnip)
                {
                    return ChildNodes.IndexOf(ChildNode);
                }
            }
            return -1;
        }
    }
}
